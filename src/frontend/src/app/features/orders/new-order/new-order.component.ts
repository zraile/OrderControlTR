import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MenuService } from '../../../core/services/menu.service';
import { OrderService } from '../../../core/services/order.service';
import { TableService } from '../../../core/services/table.service';
import { MenuCategory } from '../../../core/models/menu-category.model';
import { MenuItem } from '../../../core/models/menu-item.model';
import { TableStatus } from '../../../core/models/table.model';
import { OrderType } from '../../../core/models/order.model';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { CurrencyTrPipe } from '../../../shared/pipes/currency-tr.pipe';

interface CartItem {
  menuItemId: number;
  name: string;
  price: number;
  quantity: number;
  notes: string;
}

@Component({
  selector: 'app-new-order',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent, CurrencyTrPipe],
  templateUrl: './new-order.component.html'
})
export class NewOrderComponent implements OnInit {
  route = inject(ActivatedRoute);
  router = inject(Router);
  menuService = inject(MenuService);
  orderService = inject(OrderService);
  tableService = inject(TableService);

  tableId = 0;
  categories: MenuCategory[] = [];
  items: MenuItem[] = [];
  filteredItems: MenuItem[] = [];
  cart: CartItem[] = [];
  selectedCategory: number | null = null;
  orderNotes = '';
  loading = false;
  submitting = false;

  ngOnInit() {
    this.tableId = +this.route.snapshot.params['tableId'];
    this.menuService.getCategories().subscribe(cats => this.categories = cats);
    this.menuService.getItems().subscribe(items => {
      this.items = items.filter(i => i.isAvailable);
      this.filteredItems = this.items;
    });
  }

  selectCategory(categoryId: number | null) {
    this.selectedCategory = categoryId;
    if (categoryId === null) this.filteredItems = this.items;
    else this.filteredItems = this.items.filter(i => i.menuCategoryId === categoryId);
  }

  addToCart(item: MenuItem) {
    const existing = this.cart.find(c => c.menuItemId === item.id);
    if (existing) existing.quantity++;
    else this.cart.push({ menuItemId: item.id, name: item.name, price: item.price, quantity: 1, notes: '' });
  }

  updateQty(item: CartItem, delta: number) {
    item.quantity += delta;
    if (item.quantity <= 0) this.cart = this.cart.filter(c => c !== item);
  }

  removeFromCart(item: CartItem) {
    this.cart = this.cart.filter(c => c !== item);
  }

  get total(): number {
    return this.cart.reduce((sum, i) => sum + i.price * i.quantity, 0);
  }

  submitOrder() {
    if (this.cart.length === 0) return;
    this.submitting = true;

    const orderDto = {
      tableId: this.tableId || undefined,
      branchId: 1,
      orderType: OrderType.DineIn,
      notes: this.orderNotes,
      totalAmount: this.total
    };

    this.orderService.create(orderDto).subscribe({
      next: (order) => {
        const addItems = this.cart.map(c =>
          this.orderService.addOrderItem({
            orderId: order.id,
            menuItemId: c.menuItemId,
            quantity: c.quantity,
            unitPrice: c.price,
            totalPrice: c.price * c.quantity,
            notes: c.notes
          })
        );

        if (this.tableId) {
          this.tableService.updateStatus(this.tableId, TableStatus.Occupied).subscribe();
        }

        Promise.all(addItems.map(obs => obs.toPromise())).then(() => {
          this.router.navigate(['/orders']);
        });
      },
      error: () => this.submitting = false
    });
  }

  cancel() {
    this.router.navigate(['/tables']);
  }
}
