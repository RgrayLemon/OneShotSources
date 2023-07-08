window.addEventListener('load', ()=>{

    document.body.addEventListener("click", function(){
        let audio = document.getElementById("audio");
        audio.load();
        audio.play();
    });

    document.getElementById("movePage1Button").addEventListener("click", function(){
        location.href = '../html/page1.html';
    })
});