<?php
class topbar{
	
	function menu(){
		?>
		<html>
		<head>
		     <link rel="stylesheet" type="text/css" href="stylesheet.css">
		</head>
		
		<nav>
		  <ul>
		   <li><a href="Home.html"> Home </a> </li>
		   <li><a href="AboutMe.html">About Me</a> </li>
		   <li> <a href="#"> Projects</a> 
		    <ul>
			  <li><a href="#"> p1 </a> </li>
			  <li><a href="#"> p2 </a> </li>
			</ul>
		   </li>
		   <li><a href="Contact.html"> Contact </a> </li>
		   <ul> 
		      <li><a href="#"> Email </a> </li>
			  <li><a href="#"> Social Media </a></li>
		   </ul>
			   <li><a href="GuestBook.html"> Guest Book </a> </li>
		   </ul>
	    </nav>
		
		<?php
	}
}
	



?>