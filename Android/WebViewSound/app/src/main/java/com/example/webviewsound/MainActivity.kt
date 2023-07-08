package com.example.webviewsound

import android.annotation.SuppressLint
import android.media.AudioAttributes
import android.media.SoundPool
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.webkit.WebView

class MainActivity : AppCompatActivity() {
    @SuppressLint("ClickableViewAccessibility")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val webView = findViewById<WebView>(R.id.webView)
        webView.settings.javaScriptEnabled = true
        webView.loadUrl("file:///android_asset/html/page1.html")

        val audioAttributes = AudioAttributes.Builder().setUsage(AudioAttributes.USAGE_MEDIA)
            .setContentType(AudioAttributes.CONTENT_TYPE_MUSIC)
            .build()

        val soundPool = SoundPool.Builder().setAudioAttributes(audioAttributes).setMaxStreams(1).build()
        val soundOne = soundPool.load(this, R.raw.cursor, 1)

        webView.setOnTouchListener { _, _ ->
            soundPool.play(soundOne, 1.0f, 1.0f, 0, 0, 1.0f)
            false
        }
    }
}