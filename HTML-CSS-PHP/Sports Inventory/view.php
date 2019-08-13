<?php
/*BTI320A
10/29/14

Student Declaration

I/we declare that the attached assignment is my/our own work in accordance with Seneca Academic Policy.
No part of this assignment has been copied manually or electronically from any other source (including web sites) or distributed to other students.

Name: Ebaad Ali
Student ID: 068-452-135
*/
?>
<?php
session_start();

if(!isset($_SERVER['HTTPS']) || $_SERVER['HTTPS'] == ""){
	$redirect = "https://".$_SERVER['HTTP_HOST'].$_SERVER['REQUEST_URI'];
	header("Location: $redirect");
}

if (!isset($_SESSION['login'])){
	header("Location: https://server.com/assign2/login.php");
	exit();
}

include "library.php";
$modz = new modify();
$menu = new headline();
$connection = new database_connect();
$connection->connect();
$sort = 'id'; 

//search logic

if(isset($_GET['sorting']) && isset($_SESSION['sort'])){
	$sort = $_GET['sorting'];
	$_SESSION['sort'] = $sort;
}
else if(isset($_GET['sorting']) && !isset($_SESSION['sort'])){
	$sort = $_GET['sorting'];
	$_SESSION['sort'] = $sort;
}
else if(!isset($_GET['sorting']) && isset($_SESSION['sort'])){  
	if(!empty($_SESSION['sort'])){
		$sort = $_SESSION['sort'];
		}
}

$search = $modz->search();

if(isset($_GET['done'])){
	unset($_SESSION['searched']);
}
if(isset($_GET['done']) || (!isset($_GET['id']) && isset($_GET['done']))){
	$search = "";
}

$query = "select * from inventory where description like '%{$search}%' order by {$sort}";
$result = $connection->q($query);


?>
<html>
	<head>
		<title> Sports Galore! : Inventory</title>
	</head>
	<body>
		<h3>Welcome Sports Galore!</h3>
		<br>

<?php 	$menu->header(); ?>
		<br>

<?php
	$set = new sort();
	$set->sortIn($result);
	$connection->close();
	$menu->footer();
?>
