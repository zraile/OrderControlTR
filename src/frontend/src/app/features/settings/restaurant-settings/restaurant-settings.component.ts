import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RestaurantService } from '../../../core/services/restaurant.service';
import { BranchService } from '../../../core/services/branch.service';
import { Restaurant } from '../../../core/models/restaurant.model';
import { Branch } from '../../../core/models/branch.model';
import { NavbarComponent } from '../../../shared/components/navbar/navbar.component';

@Component({
  selector: 'app-restaurant-settings',
  standalone: true,
  imports: [CommonModule, FormsModule, NavbarComponent],
  templateUrl: './restaurant-settings.component.html'
})
export class RestaurantSettingsComponent implements OnInit {
  restaurantService = inject(RestaurantService);
  branchService = inject(BranchService);

  restaurant: Restaurant | null = null;
  branches: Branch[] = [];
  restaurantForm: any = {};
  branchForm = { name: '', address: '', phoneNumber: '', restaurantId: 1 };
  showBranchForm = false;
  saved = false;

  ngOnInit() {
    this.restaurantService.getAll().subscribe(restaurants => {
      if (restaurants.length > 0) {
        this.restaurant = restaurants[0];
        this.restaurantForm = { ...this.restaurant };
      }
    });
    this.branchService.getAll().subscribe(branches => this.branches = branches);
  }

  saveRestaurant() {
    if (!this.restaurant) {
      this.restaurantService.create(this.restaurantForm).subscribe(r => { this.restaurant = r; this.saved = true; });
    } else {
      this.restaurantService.update(this.restaurant.id, this.restaurantForm).subscribe(r => { this.restaurant = r; this.saved = true; });
    }
    setTimeout(() => this.saved = false, 3000);
  }

  saveBranch() {
    this.branchService.create({ ...this.branchForm, restaurantId: this.restaurant?.id || 1 }).subscribe(() => {
      this.branchService.getAll().subscribe(b => this.branches = b);
      this.showBranchForm = false;
      this.branchForm = { name: '', address: '', phoneNumber: '', restaurantId: 1 };
    });
  }

  deleteBranch(branch: Branch) {
    if (confirm(`"${branch.name}" şubesini silmek istediğinize emin misiniz?`)) {
      this.branchService.delete(branch.id).subscribe(() => this.branches = this.branches.filter(b => b.id !== branch.id));
    }
  }
}
