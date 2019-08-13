<?php

session_start();

include 'library.php';
$invalid="";


	if(isset($_POST['submit'])){
	
				$user=$_POST['userID'];
				$password=$_POST['password'];
				$connection = new database_connect();
				$menu = new headline();
				$link = $connection->connect();
	//assign user to id
		$user= $_POST['userID'];
		
		if(isset($_SESSION['userID'])){
				header("Location:www.server.com/~bti320_153a34/a2/viewPlaces.php");
				echo "You're already logged in!!";
				
			}
			else{
				$user = mysqli_real_escape_string($link,$user);
				$user = html_entity_decode($user);
				$password=mysqli_real_escape_string($link,$password);
				$password= html_entity_decode($_password);
				$password=md5($password);
				
				//if textboxes arent empty
				if(!($user="" && $password="")){
					$query="SELECT * FROM user";
					$result = $connection->q($query);
					
					
					while($row=mysqli_fetch_assoc($result)){
					$DBusername = $row['userID'];
					$DBpassword= $row['password'];
					
					//then, succesful login
					if($password==md5($DBpassword)){
							$_SESSION['userID']= $user;
							header("Location:viewPlaces.php");
			}else{
				echo "Incorrect Username or Password";
			  }
				
			  }
		}
		else{
                      echo "Incorrect Username or Password";
		}
     	}
}
?>

	
<html>
	<head>
		<title>Login Page</title>
	</head>
	<body>
		<h3>Welcome to The Place Recommender-er!</h3>
		<h4>Login:</h4>
		<table>
			<form method="post">
				<tr><?php echo $invalid; ?></tr>
				<tr>
					<td>Username: <input type="text" name="userID" value="<?php if (isset($_POST['userID'])) echo trim($_POST['userID']);					   ?>"> <br><br></td>
				</tr>
				<tr>
					<td>Password: <input type="password" name="password" value="<?php if (isset($_POST['password'])) echo trim($_POST['password']);?>"> <br><br></td>
				</tr>
				<tr>
					<td><input type="submit" value="Login"></td>
				</tr>
			</form>
		</table>
	</body>
	</html>

<?php
$menu=new headline();
$menu->footer();
?>

