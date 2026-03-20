import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MenuService } from '../../../core/services/menu.service';
import { MenuCategory } from '../../../core/models/menu-category.model';
import { MenuItem } from '../../../core/models/menu-item.model';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';
import { CurrencyTrPipe } from '../../../shared/pipes/currency-tr.pipe';

@Component({
  selector: 'app-menu-management',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent, CurrencyTrPipe],
  templateUrl: './menu-management.component.html'
})
export class MenuManagementComponent implements OnInit {
  menuService = inject(MenuService);

  categories: MenuCategory[] = [];
  items: MenuItem[] = [];
  selectedCategory: MenuCategory | null = null;
  filteredItems: MenuItem[] = [];

  showCategoryForm = false;
  showItemForm = false;
  editingItem: MenuItem | null = null;

  categoryForm = { name: '', sortOrder: 1, restaurantId: 1 };
  itemForm: { name: string; description: string; price: number; menuCategoryId: number; imageUrl: string; isAvailable: boolean } = { name: '', description: '', price: 0, menuCategoryId: 0, imageUrl: '', isAvailable: true };

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.menuService.getCategories().subscribe(cats => this.categories = cats);
    this.menuService.getItems().subscribe(items => {
      this.items = items;
      this.filterItems();
    });
  }

  selectCategory(cat: MenuCategory | null) {
    this.selectedCategory = cat;
    this.filterItems();
  }

  filterItems() {
    if (!this.selectedCategory) this.filteredItems = this.items;
    else this.filteredItems = this.items.filter(i => i.menuCategoryId === this.selectedCategory!.id);
  }

  saveCategory() {
    this.menuService.createCategory(this.categoryForm).subscribe(() => {
      this.loadData();
      this.showCategoryForm = false;
      this.categoryForm = { name: '', sortOrder: 1, restaurantId: 1 };
    });
  }

  deleteCategory(cat: MenuCategory) {
    if (confirm(`"${cat.name}" kategorisini silmek istediğinize emin misiniz?`)) {
      this.menuService.deleteCategory(cat.id).subscribe(() => this.loadData());
    }
  }

  editItem(item: MenuItem) {
    this.editingItem = item;
    this.itemForm = { name: item.name, description: item.description || '', price: item.price, menuCategoryId: item.menuCategoryId, imageUrl: item.imageUrl || '', isAvailable: item.isAvailable };
    this.showItemForm = true;
  }

  saveItem() {
    if (this.editingItem) {
      this.menuService.updateItem(this.editingItem.id, this.itemForm).subscribe(() => {
        this.loadData();
        this.closeItemForm();
      });
    } else {
      this.menuService.createItem({ ...this.itemForm, menuCategoryId: this.selectedCategory?.id || this.itemForm.menuCategoryId }).subscribe(() => {
        this.loadData();
        this.closeItemForm();
      });
    }
  }

  deleteItem(item: MenuItem) {
    if (confirm(`"${item.name}" ürününü silmek istediğinize emin misiniz?`)) {
      this.menuService.deleteItem(item.id).subscribe(() => this.loadData());
    }
  }

  closeItemForm() {
    this.showItemForm = false;
    this.editingItem = null;
    this.itemForm = { name: '', description: '', price: 0, menuCategoryId: 0, imageUrl: '', isAvailable: true };
  }
}
