let x = 0;
let y = 0;
let myChart;

window.addEventListener('load', async() =>{
    // 描画処理を周期実行
    setInterval(
        () => counterForDraw()
        , 100);
    
});

function counterForDraw(){
    if(myChart){ myChart.destroy(); } // 前の描画を消さないと再度グラフが描けない
    // 点を動かすために値を変える
    if(x != 20 && y == 0){
        x += 1;
    }
    else if(x == 20 && y != 20){
        y += 1;
    }
    else if(y == 20 && x != 0){
        x -= 1;
    }
    else if(x == 0 && y != 0){
        y -= 1;
    }
    drawSpectrum(x, y); // Chart.jsでグラフを書く関数に毎度座標の値を変えて渡す
}

function drawSpectrum(x_value, y_value) { 
    const ctx = document.getElementById('chart'); 
    myChart = new Chart(ctx, { 
        plugins: [{ afterDraw: (chart) =>{ 
            drawLabel(chart, x_value, y_value); // 文字列を描かせる
        } }],
        type: 'scatter', // 散布図
        data: { 
            datasets: [
                {
                data: [{x: x_value, y: y_value}], // 引数で渡されたx座標y座標
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
                    max: 30, // 軸最大値 30 -> 20にすると文字列が範囲外になる
                    min: -10, // 軸最小値
                    display: true,
                    ticks: {
                        display: false // 軸の値非表示
                    }
                }, 
                y: { // x軸と同様
                    max: 30,
                    min: -10,
                    display: true,
                    ticks: {
                        display: false
                    }
                } 
            },
            
        }, 
    });
}

function drawLabel(chart, x_value, y_value){ 
    console.log("drawLabel");
    let x_scale = chart.scales.x;
    let y_scale = chart.scales.y;
    
    var cvs = document.getElementById(chart.canvas.id);
    var ctx = cvs.getContext('2d');
    ctx.save();
    let x_pos1 = x_scale.getPixelForValue(x_value);
    let y_pos1 = y_scale.getPixelForValue(y_value);
    ctx.beginPath();
    ctx.font = '14px serif';
    // 文字列のサイズを取得
    var label_size = ctx.measureText("点P"); 
    let label_width = label_size.width;
    let label_height = label_size.actualBoundingBoxAscent + label_size.actualBoundingBoxDescent;

    let x_point = x_pos1 + 5;
    let y_point = y_pos1 - 5;
    // はみ出してないかチェック
    if(x_point + label_width > x_scale.width){
        x_point = x_pos1 -5 - label_width;
    }
    if(y_point - label_height < 0){
        y_point = y_pos1 + 5 + label_height;
    }

    ctx.fillText("点P", x_point, y_point);
    ctx.restore(); 
}
