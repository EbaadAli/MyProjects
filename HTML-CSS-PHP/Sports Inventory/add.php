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


	include "library.php";
	$connection = new database_connect();
	$menu = new headline();
	$modz = new modify();
	$connection->connect();

	$itemnameErr ="";
	$descriptionErr ="";
	$supcodeErr ="";
	$costErr ="";
	$sellpriceErr = "";
	$onhandErr ="";
	$pointErr ="";


	if ($_POST)	{ 
		$dataValid = $modz->check_regex();
		if(isset($_GET['mod']))
				{
					if($_POST)
					{
							if($dataValid)
							{
								
									
											$_SESSION['itemname'] = $_POST['name']; 
											$_SESSION['description']  = $_POST['description'];
											$_SESSION['supplierCode'] = $_POST['code'];
											$_SESSION['cost']     = $_POST['cost'];
											$_SESSION['price']    = $_POST['price'];
											$_SESSION['onHand']   = $_POST['onhand'];
											$_SESSION['reorderPoint']  = $_POST['point'];

											if(isset($_POST['onorder']))
											{
												$_SESSION['backOrder'] = 'checked';
												$checked = 'y';
											}
											else
											{
												$_SESSION['backOrder'] = '';
												$checked = 'n';
											}
											
											$query = 'update inventory set itemName="'. $_SESSION['itemname'] .'", supplierCode="'. $_SESSION['supplierCode'] .'", 
											description="'. $_SESSION['description'] .'", onHand="'. $_SESSION['onHand'] .'", reorderPoint="'. $_SESSION['reorderPoint'] .'", 
											cost="'. $_SESSION['cost'] .'", price="'. $_SESSION['price'] .'", backOrder = "'. $checked .'" where id="'. $_GET['mod'] .'"';
											$result = $connection->q($query);
											unset($_SESSION['search_set']);
											$connection->close();
											header('Location:view.php');
					
							}	
					}
				}
		if($dataValid && !isset($_GET['mod'])) // if validations pass values,those are insrted intodatabase table
		{
			$query = 'INSERT INTO inventory set itemName="' . $_POST['name'] . '", description="' . $_POST['description'] . '", supplierCode="' . $_POST['code'] . '", cost="' . $_POST['cost'] . '",price="' . $_POST['price'] . '", onHand="' . $_POST['onhand'] . '", reorderPoint="' . $_POST['point'] . '", backOrder="' . $no . '"';
			//Run sql query - to insert data in database
 			$result = $connection->q($query);
 		header("Location: view.php");
		}
}

if(!$_POST || !$dataValid)//this is text area in a form and error messages are shown here.
{?>

<?php
if(isset($_GET['mod']))
{
	$modz->modzz($_GET['mod']);
}
?>
<html>
	<head>
	<title> Sports Galore: curling not included.</title>
	<h3>Welcome Sports Galore!</h3>
	</head>
	<body>
		<?php $menu->header(); ?>
		</br><form method = "post" action = "">
			<table>
				<tr>
					<?php
					if(isset($_GET['mod']))
					{
						$_POST['name'] = $_SESSION['itemname'];
						$_POST['description'] = $_SESSION['description'];
						$_POST['code'] = $_SESSION['supplierCode'];
						$_POST['cost'] = $_SESSION['cost'];
						$_POST['price'] =  $_SESSION['price'];
						$_POST['onhand'] = $_SESSION['onHand'];
						$_POST['point'] = $_SESSION['reorderPoint'];
					?>
						<tr>
							<td>ID</td>
							<td ><input type="text" name="modify_id" value="<?php echo $_GET['mod'];?>" readonly="readonly"/></td>
						</tr>
					<?php
					}
					?>
						<tr>
							<td>Item name:</td>
							<td> <input type="text" name="name" value="<?php if (isset($_POST['name'])) echo $_POST['name']; ?>"><?php echo $itemnameErr;?></td>
						</tr>
						<tr>
							<td>Description:</td>
							<td> <textarea  name="description" cols="22" rows="5"><?php if (isset($_POST['description'])) echo $_POST['description']; ?></textarea><?php echo $descriptionErr;?>
						</td>
						</tr>
						<tr>
							<td>Supplier Code:</td>
							<td><input type="text" name="code"value="<?php if (isset($_POST['code'])) echo $_POST['code']; ?>"><?php echo $supcodeErr;?></td>
						</tr>
						<tr>
							<td>Cost:</td>
							<td><input type="text" name="cost" value="<?php if (isset($_POST['cost'])) echo $_POST['cost']; ?>"><?php echo $costErr;?></td>
						</tr>
						<tr>
							<td>Selling price:</td>
							<td><input type="text" name="price" value="<?php if (isset($_POST['price'])) echo $_POST['price']; ?>"><?php echo $sellpriceErr;?></td>
						</tr>
						<tr>
							<td>Number on hand:</td>
							<td><input type="text" name="onhand" value="<?php if (isset($_POST['onhand'])) echo $_POST['onhand']; ?>"><?php echo $onhandErr;?></td>
						</tr>
						<tr>
							<td>Reorder Point:</td>
							<td><input type="text" name="point" value="<?php if (isset($_POST['point'])) echo $_POST['point']; ?>"><?php echo $pointErr;?></td>
						</tr>
						<tr>
							<td>On Back Order:</td>
							<td><input type="checkbox" name="onorder"<?php 
								if(isset($_GET['mod']))
								{
									echo $_SESSION['backOrder'];
								}
								else
								{
									if(isset($_POST['onorder'])&&$onorder == "onorder"){echo "checked = 'checked'";}
								}
							?>> 
							</td>
						</tr>
						<tr>
							<td><input value = "submit" type="submit"></td>
						</tr>
			</table>
		</form>
<?php
}
?>
<?php
	$menu->footer();
?>

</body>
</html>