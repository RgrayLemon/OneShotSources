let myChart;
window.addEventListener("load", ()=>{
    drawGraph(0, 0);

    // テーブル作成
    let scrollTableView = document.getElementById("scrollTableView");
    for(let i=0;i<16;i++){
        let row = document.getElementById("scrollTable").tBodies[0].insertRow(-1);
        row.insertCell(0).appendChild(document.createTextNode(i));
        row.insertCell(1).appendChild(document.createTextNode("a"));
        row.insertCell(2).appendChild(document.createTextNode("b"));
        row.insertCell(3).appendChild(document.createTextNode("c"));
        row.insertCell(4).appendChild(document.createTextNode("d"));
        row.insertCell(5).appendChild(document.createTextNode("e"));
        row.insertCell(6).appendChild(document.createTextNode("f"));
    }

    let layerToggle = document.getElementById("upperLayerToggle");
    layerToggle.addEventListener("click", ()=>{
        if(/ON/.test(layerToggle.innerText)){
            document.getElementById("upperLayer").style.display = "block";
            layerToggle.innerText = "上のレイヤー OFF";
        }
        else{
            document.getElementById("upperLayer").style.display = "none";
            layerToggle.innerText = "上のレイヤー ON";
        }
    })

    // 上の要素でクリックして下のテーブルのインデックスを取得
    document.getElementById("upperLayer").addEventListener("click", (e)=>{
        let rect = e.target.getBoundingClientRect();
        let y = e.clientY - rect.top + document.getElementById("scrollTableView").scrollTop;
        let table = document.getElementById("scrollTable");
        let index = Math.floor(y / table.tBodies[0].rows[0].offsetHeight) - 1;
        document.getElementById("yTablePoint").innerText = y;
        document.getElementById("tableIndex").innerText = index;
    });

    // 上の要素のスクロールで下のテーブルをスクロール
    document.getElementById("upperLayer").addEventListener("pointermove", (e)=>{
        scrollTableView.scrollTop -= e.movementY;
    });

    // 行単位での処理
    let editTable = document.getElementById("editTable");
    for(let i=0;i<6;i++){
        let row = editTable.insertRow(-1);
        row.insertCell(0).appendChild(document.createTextNode(i+1));

        let textBox = document.createElement("input");
        textBox.setAttribute("type", "text");
        textBox.setAttribute("class", "editTextBox");
        textBox.setAttribute("value", `テスト${i+1}`);

        let deleteButton = document.createElement("button");
        deleteButton.setAttribute("type", "button")
        deleteButton.innerText = "×";

        let box = document.createElement("div");
        box.setAttribute("class", "editBox");
        box.appendChild(textBox);
        box.appendChild(deleteButton)

        row.insertCell(1).appendChild(box);

        deleteButton.addEventListener("click", function(){
            textBox.value = "";
        });
    }

    // グラフエリアをクリックして座標取得
    document.getElementById("chart").addEventListener("click", (e)=>{
        // グラフ上における座標
        let rect = e.target.getBoundingClientRect();
        let x = e.clientX - rect.left;
        let y = e.clientY - rect.top;

        // 座標変換
        let xScale = myChart.scales.x;
        let yScale = myChart.scales.y;
        let xValue = xScale.getValueForPixel(x);
        let yValue = yScale.getValueForPixel(y);

        // 四捨五入 座標表示
        document.getElementById("xPoint").innerText = Math.round(xValue);
        document.getElementById("yPoint").innerText = Math.round(yValue);

        // クリック位置に点を移動
        if(myChart){ myChart.destroy(); } 
        drawGraph(xValue, yValue);
    });

    let selectBox = document.getElementById("select");
    selectBox.selectedIndex = -1;
    selectBox.addEventListener("change", ()=>{
        document.getElementById("selectContent").innerText = `選択項目：${selectBox.value}`
        selectBox.selectedIndex = -1;
    });

});

function drawGraph(xValue, yValue){
    const ctx = document.getElementById('chart'); 
    myChart = new Chart(ctx, { 
        type: 'scatter', // 散布図
        data: { 
            datasets: [
                {
                data: [{x:xValue, y: yValue}], // 引数で渡されたx座標y座標
                backgroundColor: "#000000" // 点の色
            }],
        },
        options: {
            animation: false,
            plugins:{
                legend: {
                    display: false // 凡例なし
                }
            },
            scales: {
                x: {
                    max: 15, // 軸最大値
                    min: -15, // 軸最小値
                    display: true,
                    ticks: {
                        display: true // 軸の値非表示
                    }
                }, 
                y: { // x軸と同様
                    max: 15,
                    min: -15,
                    display: true,
                    ticks: {
                        display: true
                    }
                } 
            },
            
        }, 
    });
}