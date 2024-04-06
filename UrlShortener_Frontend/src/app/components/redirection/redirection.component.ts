import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UrlService } from '../../services/url.service';

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
    this.urlService.getRedirectionUrl(shortUrl) 
    .subscribe(response =>{ LongUrl = response
      console.log(response)
    })
    console.log(this.longUrl);
    if (LongUrl) {
      window.open(this.getLongUrlFromShortCode(this.longUrl));
    }
  }

  private getLongUrlFromShortCode(shortUrl: string): string {
    this.urlService.getRedirectionUrl(shortUrl)
      .subscribe(response => this.longUrl = response);
    return this.longUrl;
  }
}
