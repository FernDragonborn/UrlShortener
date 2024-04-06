import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UrlService } from '../../services/url.service';
import { enviroment } from '../../enviroments/enviroment';

@Component({
  selector: 'app-url-redirection',
  templateUrl: './redirection.component.html',
  styleUrl: './redirection.component.sass'
})
export class RedirectionComponent implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router, private urlService: UrlService) {}
  longUrl: string = '' 

  ngOnInit(): void {
    const shortUrl : string | null = this.route.snapshot.paramMap.get('shortCode');
    let LongUrl: string | undefined;
    window.open(enviroment.baseApiUrl + '/api/redirect/' + shortUrl);
    window.close();
  }
}
