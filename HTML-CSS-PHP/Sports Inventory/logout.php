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
	 session_destroy();
	 setcookie("PHPSESSID", "", time() - 61200,"/");

	 header("Location: https://server.com/assign2/login.php");
?>