export class UrlDto{
    shortUrl: string = '';
    longUrl: string = '';
    userId: string | null = '';
    createdAt: Date | null = null;
    clicks: number = 0;
}