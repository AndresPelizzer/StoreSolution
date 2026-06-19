export interface Giocatore {
  idgiocatore: number;
  nome: string;
  cognome: string;
  ruolo: string;
  idsquadra: number;
}
export interface Squadra {
  idsquadra: number;
  nomeSquadra: string;
  citta: string;
  allenatore: string;
  numeroGiocatoriInRosa: number;
  giocatori: Giocatore[];
}
export interface Utenti {
  id: number;
  nome: string;
  username: string;
  email: string;
  ruolo: string;
  password: string;
  note?: string;
}
export interface Credenziali {
  username: string;
  password: string;
}
