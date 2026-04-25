import {inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {APP_CONFIG, AppConfig} from '../../app.config';
import {Observable} from 'rxjs';
import {Product} from '../models/product.model';
import {Module} from '../models/module.model';


@Injectable({
  providedIn: 'root'
})
export class ProductService {

  private readonly http: HttpClient = inject(HttpClient);
  private readonly config: AppConfig = inject(APP_CONFIG);
  private readonly baseURL:string =  `${this.config.apiBaseUrl}/Products`;

  public getProducts$(): Observable<Product[]>{
    return this.http.get<Product[]>(this.baseURL);
  }

  public getModulesByProducts(productId:number | null): Observable<Module[]> {
    return this.http.get<Module[]>(`${this.baseURL}/${productId}/module`);
  }
}
