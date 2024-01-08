import { Component } from '@angular/core';
import { AppRoutingModule } from '../app.routes'

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [AppRoutingModule],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

}
