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
	include "library.php";

 	session_start();
	if (!isset($_SESSION['login']))
	{
			header("Location: https://server.com/assign2/login.php");
 			exit();
	}
	if(!isset($_SERVER['HTTPS']) || $_SERVER['HTTPS'] == "")
	{
    	$redirect = "https://".$_SERVER['HTTP_HOST'].$_SERVER['REQUEST_URI'];
    	header("Location: $redirect");
	}
	else
	
		$connection = new database_connect();
		$connection->connect();
		$id = $_GET['id'];
		$_SESSION['delete_id'] = $_GET['id'];
		$query = "select * from inventory where id = {$id}";
		$result = $connection->q($query);
		$row = mysqli_fetch_assoc($result);

			if($row['deleted'] == 'y')
			{
				$query = "update inventory set deleted='n' WHERE id={$id}";
			}
			if($row['deleted'] == 'n')
			{
				$query = "update inventory set deleted='y' WHERE id={$id}";
			}

			$result = $connection->q($query);
			if(isset($_GET['done'])){
				$page = "view.php?done=1";
			}else{
				$page = "view.php";
			}
			header("Location: $page");
?>
