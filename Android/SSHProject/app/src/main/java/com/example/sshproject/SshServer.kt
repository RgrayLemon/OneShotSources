package com.example.sshproject

import android.os.Build
import android.os.Handler
import android.os.Looper
import android.util.Log
import androidx.annotation.RequiresApi
import org.apache.sshd.common.keyprovider.FileKeyPairProvider
import org.apache.sshd.common.util.security.bouncycastle.BouncyCastleGeneratorHostKeyProvider
import org.apache.sshd.server.SshServer
import java.security.Security
import org.bouncycastle.jce.provider.BouncyCastleProvider
import java.util.concurrent.ExecutorService
import java.util.concurrent.Executors
import java.nio.file.Paths

object SshServer {
    private val TAG = "SshServer"
    val serverKeyPath = "hostkey.ser"

    @RequiresApi(Build.VERSION_CODES.O)
    fun start(){
        System.setProperty("user.home", "/data/data/com.example.sshproject/")

        Security.removeProvider("BC")
        Security.addProvider(BouncyCastleProvider())
        val server = SshServer.setUpDefaultServer()
        server.port = 22222

        server.keyPairProvider = BouncyCastleGeneratorHostKeyProvider(Paths.get(serverKeyPath))
        //server.keyPairProvider = FileKeyPairProvider()

        val asyncProgress = SshServerStartTask(server)
        asyncProgress.execute()
        Log.d(TAG, "ssh server start!!!")
    }

    private class SshServerStartTask(val server: SshServer) {
        var executorService: ExecutorService = Executors.newSingleThreadExecutor()

        private inner class TaskRun : Runnable {
            override fun run() {
                try {
                    server.start()
                }
                catch (e:Exception){
                    Log.d(TAG, "ssh server start fail")
                    Log.d(TAG, e.printStackTrace().toString())
                    server.close()
                }
                Handler(Looper.getMainLooper())
            }
        }

        fun execute() {
            executorService.submit(TaskRun())
        }

    }
}