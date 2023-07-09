window.addEventListener('load', ()=>{
    let isChangePage = false;

    // 画面クリックで音を鳴らす
    document.body.addEventListener("click", function(){
        let audio = document.getElementById("audio");
        audio.load();
        audio.play();
    });

    // 音声再生が終わってから画面遷移させる
    document.getElementById("audio").addEventListener("ended", function(){
        if(isChangePage){
            location.href = '../html/page2.html';
        }
    });

    document.getElementById("movePage2Button").addEventListener("click", function(){
        isChangePage = true;
    })
});