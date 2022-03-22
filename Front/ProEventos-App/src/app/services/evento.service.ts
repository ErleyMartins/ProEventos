import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators'
import { Evento } from '../models/Evento';

@Injectable(
  //{ providedIn: 'root'}
  )
export class EventoService {

  mainURL = 'https://localhost:5001/api/Eventos';

  constructor(private http: HttpClient) { }

  public getEventos(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.mainURL).pipe(take(1));
  }

  public getEventosByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.mainURL}/tema/${tema}`).pipe(take(1));
  }

  public getEventoById(id: number): Observable<Evento> {
    return this.http.get<Evento>(`${this.mainURL}/${id}`).pipe(take(1));
  }

  public post(evento: Evento): Observable<Evento> {
    return this.http.post<Evento>(this.mainURL, evento).pipe(take(1));
  }

  public put(evento: Evento): Observable<Evento> {
    return this.http.put<Evento>(`${this.mainURL}/${evento.id}`, evento).pipe(take(1));
  }

  public deleteEvento(id: number): Observable<any> {
    return this.http.delete(`${this.mainURL}/${id}`).pipe(take(1));
  }

}
