import {Product} from './product.model';

export interface Module {
  id: number
  label: string
  products: Product[]
}
