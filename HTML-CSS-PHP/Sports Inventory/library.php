<?php
/*
BTI320A
10/29/14

Student Declaration

I/we declare that the attached assignment is my/our own work in accordance with Seneca Academic Policy.
No part of this assignment has been copied manually or electronically from any other source (including web sites) or distributed to other students.

Name: Ebaad Ali

Student ID: 068-452-135
*/
?>
<?php
class database_connect{
	private $link;
//Connects to database
	function connect(){
		$lines = file('/home/int322/secret/topsecret.txt'); //from local file not on web accessible path
		$dbserver = trim($lines[0]); //get database servername
		$uid = trim($lines[1]); //get database username
		$pw = trim($lines[2]); //get database password
		$dbname = trim($lines[3]); //get database name

//Connect to the mysql server and get back our link_identifier
		$link = mysqli_connect($dbserver, $uid, $pw, $dbname) or die('Could not connect: ' . mysqli_error());
		
		return $link;
		
		}

	function q($query){
		$result = mysqli_query($this->connect(), $query) or die('failed query: ' . mysql_error());

		return $result;
		}
		
	function close(){
		mysqli_close($this->connect());
		}
}

class modify{

	function modzz($id){
		$connection = new database_connect();
		$query = "select * from inventory where id = '". $id ."'";
		$result = $connection->q($query);
		
		while ($row = mysqli_fetch_assoc($result)){
			$_SESSION['itemname'] = $row['itemName'];
			$_SESSION['description']  = $row['description'];
			$_SESSION['supplierCode'] = $row['supplierCode'];
			$_SESSION['cost']     = $row['cost'];
			$_SESSION['price']    = $row['price'];
			$_SESSION['onHand']   = $row['onHand'];
			$_SESSION['reorderPoint']  = $row['reorderPoint'];
			$_SESSION['backOrder']  = $row['backOrder'];
			$_SESSION['delete'] = $row['deleted'];
			
			if($_SESSION['backOrder'] == 'y'){
				$_SESSION['backOrder'] = 'checked';
				}
				else if($_SESSION['backOrder'] == 'n'){
					$_SESSION['backOrder'] = '';
					}
				}
			}
			
	function search(){
		// search logic
		$search = ''; //default
		
		if(isset($_POST['desc']) && $_POST['desc'] =='') {
			unset($_SESSION['searched']);
			}
			if(isset($_POST['desc']) && $_POST['desc'] !==''){
				$search = $_POST['desc'];
				$_SESSION['searched'] = $search;
				}
				else if(isset($_POST['desc']) && !isset($_SESSION['searched'])){
					$search = $_POST['desc'];
					$_SESSION['searched'] = $search;
					}
					else if(!isset($_POST['desc']) && isset($_SESSION['searched'])){ 
						if(isset($_SESSION['searched']) && !empty($_SESSION['searched'])){
							$search = $_SESSION['searched'];
							}
							if($_SESSION['searched'] =='' && isset($_GET['done'])){
								unset($_SESSION['searched']); $search = ''; 
								}
							}
							
							return $search;
						}
								
	function check_regex(){	
		global $itemnameErr;
		global $descriptionErr;
		global $supcodeErr;
		global $costErr;
		global $sellpriceErr;
		global $onhandErr;
		global $pointErr;
		global $check;
		$dataValid = true;
		
		if (!preg_match("/^[a-z,\-\':;0-9 ]+$/i", TRIM($_POST['name']))){
			$itemnameErr = "<font color ='red'>letters, spaces, colon, semi-colon, dash, comma, apostrophe and numbers only - cannot be blank</font>";
			$dataValid = false;
			}
			
			if (!preg_match("/^[\r\sa-z0-9.,\'\-]+$/i",trim($_POST['description']))){
				$descriptionErr = "<font color ='red'>letters, digits, periods, commas, apostrophes, dashes and spaces only - cannot be blank</font>";
				$dataValid = false;
				}
				if (!preg_match("/^[A-Z]{3}[0-9]{3,}$/",trim($_POST['code'])))  {
					$supcodeErr = "<font color ='red'>supplier code begins with three uppercase letters and is followed by three or more digists - cannot be blank</font>";
					$dataValid = false;
					}
					if (!preg_match("/^[0-9]+[.][0-9]{2}$/", trim($_POST['cost'])))  {
						$costErr = "<font color ='red'>monetary amounts only i.e. one or more digits, then a period, then two digits - cannot be blank</font>";
						$dataValid = false;
						}
						if (!preg_match("/^[0-9]+$/", trim($_POST['point'])))	{
							$pointErr = "<font color ='red'>digits only- cannot be blank</font>";
							$dataValid = false;
							}
							if (!preg_match("/^[0-9]+[.][0-9]{2}$/", trim($_POST['price'])))	{
								$sellpriceErr = "<font color ='red'>monetary amounts only i.e. one or more digits, then a period, then two digits - cannot be blank</font>";
								$dataValid = false;
								}
								if (!preg_match("/^[0-9]+$/", trim($_POST['onhand'])))	{
									$onhandErr = "<font color ='red'>digits only- cannot be blank</font>";
									$dataValid = false;
									}
									if (isset($_POST['onorder']))	{
										$onorder = $_POST['onorder'];
										$check = 'y';
										}
										if (!isset($_POST['onorder']))	{
									
											$check = 'n';
											}
											
											return $dataValid;
											
							}
            }
            
            
class headline{
	
	function session(){
		?> <b>User:</b> <?php
		echo $_SESSION['username']  ?> &nbsp;&nbsp; <?php
		?> <b>Role:</b> <?php
		echo $_SESSION['role'];
		}
		
	function header(){
?>	
<html>
	<body>
		<table>
			<form method = "post" action = "view.php">
				<tr>
					<td>
						<a href = "add.php">Add</a>
					</td>
					<td>
						<a href = "view.php?done=1"> View all</a>
					</td>
					<td>Search in Description:
						<input type = "text" value ="<?php if(isset($_GET['done'])){ $_SESSION['searched'] = '';} if(isset($_SESSION['searched'])) echo $_SESSION['searched'];?>" name = "desc"/></td>
					<td>
						<input type = "submit" value="search"/>
					</td>
					<td>
						<?php $this->session();?>
					</td>
					<td>
						&nbsp;&nbsp;<a href = "logout.php">Log Out</a>
					</td>
				</tr>
			</form>
		</table>
	</body>
		<br><br>
<?php 
				}
                     
    function footer(){ ?>
		<footer><?php echo "Ebaad Ali &copy 2014"; ?></footer>
</html>
<?php
			}
		}

class sort{
	function sortIn($result){
?>

		<table border = "1">
			<th><a href = "view.php?sorting=id">ID</a></th>
			<th><a href = "view.php?sorting=itemName">Item name</a></th>
			<th><a href = "view.php?sorting=description">Description</a></th>
			<th><a href = "view.php?sorting=supplierCode">Supplier</a></th>
			<th><a href = "view.php?sorting=cost">Cost</a></th>
			<th><a href = "view.php?sorting=price">Price</a></th>
			<th><a href = "view.php?sorting=onHand">Number On Hand</a></th>
			<th><a href = "view.php?sorting=reorderPoint">Reorder level</a></th>
			<th><a href = "view.php?sorting=backOrder">On Back Order?</a></th>
			<th><a href = "view.php">Delete/Restore</a></th>
<?php
		
		while($row = mysqli_fetch_assoc($result)){
?>
			<tr>
				<td>
					<a href = "add.php?mod=<?php echo $row['id'];?>"><?php echo $row['id'];?></a>
				</td>
				<td>
					<?php echo $row['itemName']; ?>
				</td>
				<td>
					<?php echo $row['description']; ?>
				</td>
				<td>
					<?php echo $row['supplierCode']; ?>
				</td>
				<td>
					<?php echo $row['cost']; ?>
				</td>
				<td>
					<?php echo $row['price']; ?>
				</td>
				<td>
					<?php echo $row['onHand']; ?>
				</td>
				<td>
					<?php echo $row['reorderPoint']; ?>
				</td>
				<td>
					<?php echo $row['backOrder']; ?>
				</td>
				<td><a href = "delete.php?id=<?php echo $row['id'];?>&<?php if(isset($_GET['done'])){echo "done=1";}?>">
<?php 
			if($row['deleted'] == 'y'){echo "Delete";}else if ($row['deleted'] == 'n'){echo "Restore";} ?>			  </a>
			</td>
		</tr>
<?php
					}
?>
	</table>
<?php
		}
	}
?>
