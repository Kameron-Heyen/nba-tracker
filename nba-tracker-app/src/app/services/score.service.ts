import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Game } from '../DataContracts/Game';
import { BoxScore } from '../DataContracts/BoxScore';

@Injectable({
  providedIn: 'root',
})
export class ScoreService {
  private readonly nbaApiBaseUrl = 'http://localhost:5001/api';

  constructor(private readonly http: HttpClient) {}

  getTodaysGames(): Observable<Game[]> {
    return this.http.get<Game[]>(`${this.nbaApiBaseUrl}/TodaysScores`);
  }

  getBoxScore(gameId: string): Observable<BoxScore> {
    return this.http.get<BoxScore>(`${this.nbaApiBaseUrl}/BoxScore/${gameId}`);
  }
}
