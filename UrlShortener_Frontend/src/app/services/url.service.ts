import { Injectable } from '@angular/core';
import { enviroment } from '../enviroments/enviroment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UrlDto } from '../models/url.model';

@Injectable({
  providedIn: 'root'
})
export class UrlService {
  constructor(private http: HttpClient) { }

  baseApiUrl: string = enviroment.baseApiUrl;

  getRedirectionUrl(shortUrl: string | null): Observable<string>{
    let url = this.baseApiUrl + '/api/redirect/' + shortUrl;
    console.log('request to: ' + url)
    return this.http.get<string>(url);
  }

  getUrls(): Observable<UrlDto[]>{
    return this.http.get<UrlDto[]>(this.baseApiUrl + '/api/url/getAll');
  }

  getShortUrl(urlDto: UrlDto): Observable<UrlDto>{
    return this.http.post<UrlDto>(this.baseApiUrl + '/api/url/register', urlDto);
  }
}
