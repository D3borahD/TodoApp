import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {Observable} from 'rxjs';
import {Module} from '../models/module.model';

@Injectable({
  providedIn: 'root'
})
export class ModuleService {

  private readonly http: HttpClient = inject(HttpClient);
  private readonly config: AppConfig = inject(APP_CONFIG);
  private readonly baseURL:string = `${this.config.apiBaseUrl}/Modules`;

  public getModules$(): Observable<Module[]> {
    return this.http.get<Module[]>(`${this.baseURL}`);
  }


}
