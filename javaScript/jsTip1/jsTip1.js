let myChart;
window.addEventListener("load", ()=>{
    drawGraph(0, 0);

    // グラフエリアをクリックして座標取得
    document.getElementById("chart").addEventListener("click", (e)=>{
        let rect = e.target.getBoundingClientRect();
        var x = e.clientX - rect.left;
        var y = e.clientY - rect.top;
        let xScale = myChart.scales.x;
        let yScale = myChart.scales.y;
        let xValue = xScale.getValueForPixel(x);
        let yValue = yScale.getValueForPixel(y);
        // 四捨五入
        document.getElementById("xPoint").innerText = Math.round(xValue);
        document.getElementById("yPoint").innerText = Math.round(yValue);
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
                    max: 15, // 軸最大値 30 -> 20にすると文字列が範囲外になる
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