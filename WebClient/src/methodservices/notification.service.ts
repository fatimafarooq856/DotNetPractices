import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class NotificationService {
  constructor() {}
  success(msg: string | any) {}
  error(msg: string | any) {}
  warning(msg: string | any) {}
}
