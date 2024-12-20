import { Component, inject } from '@angular/core';
import { AuthService } from '../auth/services/auth.service';
import { AppComponent } from '../../app.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {
  auth = inject(AuthService);

  get user() {      
    return this.auth.currentUser;
  }

  get userDTO() {
    return this.auth.decodedUserToken;
  }

}
