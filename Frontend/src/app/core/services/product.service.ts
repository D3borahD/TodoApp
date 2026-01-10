import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {Observable} from 'rxjs';
import {Product} from '../models/product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private readonly baseURL:string;
  constructor(
    private http: HttpClient,
    @Inject(APP_CONFIG) private config: AppConfig
  ) {
    this.baseURL = `${config.apiBaseUrl}/Products`;
  }

  public getProducts$(): Observable<Product[]>{
    return this.http.get<Product[]>(this.baseURL);
  }
}
