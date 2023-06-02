window.addEventListener('load', () => {
    document.getElementById("postButton").addEventListener('click', ()=>{
        let str = document.getElementById("inputTextBox").value;
        httpRequest(str);
    });
});

function httpRequest(str){
    let postData = new FormData();
    postData.append("str", str);
    let request = new XMLHttpRequest();
    
    request.onreadystatechange = function(){
        if(request.readyState == 4){
            console.log(request.status);
            let data = request.responseText;
            document.getElementById("response").innerText = data;
        }
    }

    request.open('POST', 'http://localhost:8080/xmlHttpRequest.php', true);
    request.send(postData);
}