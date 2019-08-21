import React, { Component } from 'react';
export default class About extends Component {
  render() {
    let resumeData = this.props.resumeData;
    return (
      <section id="about">
         <div className="row">

            <div className="three columns">

               <img className="profile-pic"  src="images/mom_and_me.jpg" alt="" />

            </div>

            <div className="nine columns main-col">

               <h2>About Me</h2>
               <p>
               I am a recent graduate of Seneca as a Computer Programmer and Analyst who has an extensive background in Java, C++ and a long list of web development frameworks/languages. I am a quick and eager learner whos references will agree that I can be an asset to any team. <br/> 
               <h2>Download my resume</h2><h3><a className="buttonDownload" href="images/Ebaad_Ali_Resume.docx" download> Here </a></h3> 

               </p>

               <div className="row">

                  <div className="columns contact-details">

                  <h2>Contact Details</h2>
                  <p className="address">
       						<span>{resumeData.name}</span>
                     <br></br>
       						   <span>
                     {resumeData.phone}
                    </span>
                    <br></br>
                    <a href="mailto:ebaadali@gmail.com">{resumeData.email}</a>
                    <br></br>
                    <a href="http://www.ebaadali.wordpress.com">{resumeData.blog}</a>
       					   </p>
                  </div>
               </div>
            </div>
         </div>
      </section>
    );
  }
}