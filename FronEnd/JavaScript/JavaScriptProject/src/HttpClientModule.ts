// Import Angular HttpClientModule in your app.module.ts first:
// import { HttpClientModule } from '@angular/common/http';

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface Claim {
  id: number;
  amount: number;
  status: 'Open' | 'Closed';
}

@Injectable({
  providedIn: 'root'
})
export class ClaimService {
  private apiUrl = 'https://api.example.com/claims';

  constructor(private http: HttpClient) {}

  // Returns Observable of claims fetched from API
  getClaims(): Observable<Claim[]> {
    return this.http.get<Claim[]>(this.apiUrl);
  }
}
