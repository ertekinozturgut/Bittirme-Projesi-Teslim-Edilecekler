<?php
   	//include("connect.php");
   	
   	//$link=Connection();
    $link = mysql_connect('94.73.170.150','user_7621258','******') or die('Cannot connect to the DB');
    mysql_select_db('db_ertechinsoftware_com',$link) or die('Cannot select the DB');
	$temp1=$_POST["temp1"];
    $hum1=$_POST["hum1"];
    $arabaad=$_POST["Araba_Ad"];
	//$query = "INSERT INTO `tempLog` (`temperature`, `humidity`) 
      //  VALUES ('".$temp1."','".$hum1."')"; 
   	$query="INSERT INTO `ArabaVerileri`(`Araba_Ad`, `Sicaklik`, `Nem`)
        VALUES (.$arabaad,.$temp1,.$hum1)";
   	mysql_query($query,$link);
	mysql_close($link);

  // 	header("Location: index.php");
?>
