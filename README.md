## DMS -Document Management System

## Technologies
C# ASP.NET core 2.1 (MVC ) , 3-tier Architechture, EF Code First

## Features
<ul>
	<li>Admin Creates Users & assign Roles</li>
	<li>User get notified by email for credentials</li>
	<li>User can creates own categories</li>
	<li>User can upload own documents</li>
	<li>User can Search & Downloads</li>
</ul>

## Screenshots
<img src="readme/1.png" alt="demo"/>
<img src="readme/2.png" alt="demo"/>
<img src="readme/4.png" alt="demo"/>
<img src="readme/5.png" alt="demo"/>

## Installation
step 1: $ git clone https://github.com/nayeemazad/DMS.git <br>
Step 2: Set gmail credential for sending mail at DMS.WebUI/appsettings.json file<br>
<pre>
"SendMail": {
    "Setting": {
      "Gmail": "your_gmail_id@gmail.com",
      "Password": "your_gmail_password",
    }
  }
</pre>
<br>
Step 3: Set ConnectionStrings for Database connection <br>
<pre>"ConnectionStrings": {
    "DmsDb": "Server={value};Database={value};Initial Catalog= {value};Integrated Security=True;User Id={value};Password={value};"
  }</pre>