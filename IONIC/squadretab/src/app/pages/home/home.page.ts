import { HttpClient } from '@angular/common/http';

import { addIcons } from 'ionicons';
import {
  lockClosedOutline,
  personOutline,
  arrowForwardOutline,
  alertCircleOutline,
} from 'ionicons/icons';

addIcons({
  'lock-closed-outline': lockClosedOutline,
  'person-outline': personOutline,
  'arrow-forward-outline': arrowForwardOutline,
  'alert-circle-outline': alertCircleOutline,
});
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import {
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonCard,
  IonCardHeader,
  IonCardContent,
  IonInput,
  IonItem,
  IonButton,
  IonIcon,
  IonText,
  NavController,
} from '@ionic/angular/standalone';
import { firstValueFrom } from 'rxjs';
import { Credenziali } from 'src/app/models/squadra.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: true,
  imports: [
    FormsModule,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonCard,
    IonCardHeader,
    IonCardContent,
    IonInput,
    IonItem,
    IonButton,
    IonIcon,
    IonText,
  ],
})
export class HomePage {
  credenziali: Credenziali = {
    username: '',
    password: '',
  };
  errore: string = '';

  async login() {
    try {
      const token = await firstValueFrom(
        this.http.post('https://localhost:7019/api/utenti', this.credenziali, {
          responseType: 'text',
        }),
      );

      if (token && token.trim() !== '') {
        localStorage.setItem('token', token);
        this.navCtrl.navigateForward('/tabs/tab1');
      } else {
        this.errore = 'Il token non é valido';
      }
    } catch (err) {
      this.errore = 'Credenziali errate';
    }
  }

  constructor(
    private http: HttpClient,
    private navCtrl: NavController,
  ) {}
}
