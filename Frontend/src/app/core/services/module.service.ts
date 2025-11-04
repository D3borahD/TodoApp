import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {Observable} from 'rxjs';
import {Module} from '../models/module.model';

@Injectable({
  providedIn: 'root'
})
export class ModuleService {
  private readonly baseURL!:string;

  constructor(
    private readonly http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.baseURL = `${config.apiBaseUrl}/Modules`;
  }

  public getModules(): Observable<Module[]> {
    return this.http.get<Module[]>(`${this.baseURL}`);
  }
}
