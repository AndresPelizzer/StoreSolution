import { Component } from '@angular/core';
import { IonicModule, NavController } from '@ionic/angular';
import { FormsModule } from '@angular/forms';
import { SquadraService } from '../../services/squadra';
import { Squadra } from '../../models/squadra.model';
import { addIcons } from 'ionicons';
import { addOutline } from 'ionicons/icons';

@Component({
  selector: 'app-aggiungi-squadra',
  templateUrl: './aggiungi-squadra.page.html',
  standalone: true,
  imports: [IonicModule, FormsModule],
})
export class AggiungiSquadraPage {
  nuovaSquadra: Squadra = {
    idsquadra: 0,
    nomeSquadra: '',
    citta: '',
    allenatore: '',
    numeroGiocatoriInRosa: 0,
    giocatori: [],
  };

  constructor(
    private squadraService: SquadraService,
    private navCtrl: NavController,
  ) {
    addIcons({ addOutline });
  }

  post() {
    if (
      !this.nuovaSquadra.nomeSquadra.trim() ||
      !this.nuovaSquadra.citta.trim() ||
      !this.nuovaSquadra.allenatore.trim()
    ) {
      alert(
        'Attenzione: devi compilare tutti i campi prima di aggiungere una squadra!',
      );
      return;
    }
    this.squadraService.postDati(this.nuovaSquadra).subscribe({
      next: () => {
        this.squadraService.notificaAggiornamento();
        this.navCtrl.back();
      },
      error: (errore: any) => console.log('Errore:', errore),
    });
  }
}
