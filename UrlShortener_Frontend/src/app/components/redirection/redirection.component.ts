import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-redirection',
  template: '<div>Redirection...</div>'
})
export class RedirectionComponent implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router) {}

  ngOnInit(): void {
    const shortCode = this.route.snapshot.paramMap.get('shortCode');
    if (shortCode) {
      this.router.navigate([this.getLongUrlFromShortCode(shortCode)]);
    }
  }

  private getLongUrlFromShortCode(shortCode: string): string {
    // Добавьте логику для получения длинного URL, связанного с коротким кодом, из вашего бэкэнда
    // Например, выполните HTTP-запрос к вашему API
    return 'http://example.com/original-url';
  }
}
