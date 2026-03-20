import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'currencyTr', standalone: true })
export class CurrencyTrPipe implements PipeTransform {
  transform(value: number): string {
    if (value == null) return '₺0,00';
    return '₺' + value.toLocaleString('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 });
  }
}
