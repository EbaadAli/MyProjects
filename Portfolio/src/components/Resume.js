import React, { Component } from 'react';
import Bars from 'react-bars';

const skills =[
  {label:'SQL', value:98},
  {label:'HTML5', value:95},
  {label:'CSS', value:90},
  {label:'Javascript (ES5/ES6)', value:80},
  {label:'Bootstrap', value:90},
  {label:'C++', value: 90},
  {label:'Java', value: 90},
] ;

const skills2=[
  {label:'ASP .NET', value: 90},
  {label:'React', value:85},
  {label:'Angular.js', value:60},
  {label:'Node.js', value:60},
  {label:'Python', value:90},
  {label:'JQuery', value:80},
  {label:'Bar to fill my OCD', value: 100}
] ;


              
export default  class Resume extends Component {
  
  render() {
    let resumeData = this.props.resumeData;
    return (
      <section id="resume">

         <div className="row education">

            <div className="three columns header-col">
               <h1><span>Education</span></h1>
            </div>

            <div className="nine columns main-col">
              {
                resumeData.education && resumeData.education.map((item)=>{
                  return(
                    <div className="row item">
                       <div className="twelve columns">
                          <h3>{item.UniversityName}</h3>
                          <p className="info">
                          {item.specialization}
                          <span>&bull;</span> <em className="date">{item.MonthOfPassing} {item.YearOfPassing}</em></p>
                          <p>
                          {item.Achievements}
                          </p>
                       </div>
                    </div>
                  )
                })
              }
            </div>
         </div>
        <div className="row work">
            <div className="three columns header-col">
               <h1><span>Work</span></h1>
            </div>

            <div className="nine columns main-col">
              {
                resumeData.work && resumeData.work.map((item) => {
                  return(
                    <div className="row item">
                       <div className="twelve columns">
                          <h3>{item.CompanyName}</h3>
                          <p className="info">
                          {item.specialization}
                          <span>&bull;</span> <em className="date">{item.MonthStarted}{item.YearStarted} - {item.MonthOfLeaving} {item.YearOfLeaving}</em></p>
                          <ul>
                           <li>{item.Achievement1}</li>
                           <li>{item.Achievement2}</li>
                           <li>{item.Achievement3}</li>
                           <li>{item.Achievement4}</li>
                          </ul>
                       </div>

                    </div>

                  )
                })
              }
            </div> 
         </div>


         <div className="row skill">

            <div className="three columns header-col">
               <h1><span>Technical Skills</span></h1>
            </div>
            <br/>
            <br/>

           <div className="bars-container">
              

   				   <div className="skills1">
                {
                  <li>
                     <Bars data={skills}/>
                  </li>
                }
              </div>
              <div className="skills2">
              {
                <li>
                      <Bars data={skills2} />
                  </li>
              }
                  
              </div>

   					</div>

   				

   			</div>


      </section>
    );
  }
}