<?php
   	//include("connect.php");
       //$link=Connection();
  if(isset($_GET['temp1']) && isset($_GET['hum1']) && isset($_GET["arabaad"])) {
    echo "sdsdfgs";

    $link = mysql_connect('94.73.170.150','user_7621258','*****') or die('Cannot connect to the DB');


    mysql_select_db('db_ertechinsoftware_com',$link) or die('Cannot select the DB');
	$temp1=$_GET['temp1'];
    $hum1=$_GET['hum1'];
   $arabaad=$_GET['arabaad'];
    //
    echo $temp1;
    echo $hum1;
    echo $arabaad;
	//$query = "INSERT INTO `tempLog` (`temperature`, `humidity`) 
      //  VALUES ('".$temp1."','".$hum1."')"; 
   $query="INSERT INTO ArabaVerileri (`Araba_Ad`, `Sicaklik`, `Nem`) VALUES ('".$arabaad."','.$temp1.','.$hum1.')";
   	mysql_query($query,$link);
	mysql_close($link);
    }
    else
    {
        echo "hata var";
    }
  // 	header("Location: index.php");
?>
