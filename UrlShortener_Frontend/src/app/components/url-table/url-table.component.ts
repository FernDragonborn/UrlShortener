import { UrlDto } from './../../models/url.model';
import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { UrlService } from '../../services/url.service';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ChangeDetectionStrategy } from '@angular/core';
@Component({
  selector: 'app-link-table',
  templateUrl: './url-table.component.html',
  styleUrls: ['./url-table.component.sass'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UrlTableComponent implements OnInit{
  
  constructor(private urlService: UrlService, 
    private authService: AuthService, 
    private formBuiled: FormBuilder,
    private ref: ChangeDetectorRef) { 
      setInterval(() => {
        this.numberOfTicks++;
        this.ref.markForCheck();
      }, 1000);
    }
      
    numberOfTicks: number = 0;

  isLoggedIn : Boolean = false;
  links: UrlDto[] = [];
  dto: UrlDto | undefined;
  selectedLink: UrlDto | undefined;
  urlForm: FormGroup = this.formBuiled.group({
    originalUrl: ['', [Validators.required, Validators.pattern('https?://.+')]]
  });;

  ngOnInit(): void{
    this.renderTable();

    this.isLoggedIn = this.authService.isLoggedIn()
    console.log('Is logged in: ' + this.isLoggedIn)
  }

  renderTable(): void{
    this.urlService.getUrls().
    subscribe(response => {
      this.links = response; 
      console.log('1 links: ' + this.links)
    });
    console.log('2 links: ' + this.links)
  }

  shortenUrl(): void {
    if (this.urlForm.valid) {
      const originalUrl = this.urlForm.get('originalUrl')?.value;
      this.dto = new UrlDto();
      this.dto.longUrl = originalUrl;
      this.urlService.getShortUrl(this.dto)
      .subscribe(response =>{ 
        this.links.push(response)
         console.log('long url: ' + response.longUrl)
        }) 
    }
  }

  showLinkInfo(link: UrlDto) {
    this.selectedLink = link;
  }

  hideLinkInfo() {
    this.selectedLink = undefined;
  }
}
