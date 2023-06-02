<?php 
    $data = array();
    $data['str'] = htmlspecialchars($_POST['str'],ENT_QUOTES);
    //$data['str'] = $_POST['str'];

    $reply = "応答：".$data['str'];
    echo $reply;
?>