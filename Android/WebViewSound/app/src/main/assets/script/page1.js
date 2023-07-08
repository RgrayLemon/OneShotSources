window.addEventListener('load', ()=>{

    // 画面クリックで音を鳴らす
    document.body.addEventListener("click", function(){
        let audio = document.getElementById("audio");
        audio.load();
        audio.play();
    });

    document.getElementById("movePage2Button").addEventListener("click", function(){
        location.href = '../html/page2.html';
    })
});