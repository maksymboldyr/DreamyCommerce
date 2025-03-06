import { Component, EventEmitter, Input, Output, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ProductDto } from '../../../interfaces/product-dto';
import { CategoryDto } from '../../../interfaces/category-dto';
import { SubcategoryDto } from '../../../interfaces/subcategory-dto';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [CommonModule, FormsModule, DropdownModule, ButtonModule, InputTextModule],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnChanges {
  @Input() chosenCategory: CategoryDto = { id: '', name: '' };
  @Input() chosenSubcategory: SubcategoryDto = { id: '', name: '' };

  defaultProduct: ProductDto = {
    name: '',
    description: '',
    price: 0,
    stock: 0,
    discount: 0,
    categoryId: '',
    subcategoryId: '',
    categoryName: '',
    subcategoryName: ''
  };

  @Input() product: ProductDto = this.defaultProduct;

  @Input() categories: CategoryDto[] = [];
  @Input() subcategories: SubcategoryDto[] = [];
  @Input() filteredSubcategories: SubcategoryDto[] = [];
  @Output() save = new EventEmitter<ProductDto>();
  @Output() cancel = new EventEmitter<void>();
  @Output() categoryChange = new EventEmitter<string>();

  ngOnChanges(changes: SimpleChanges) {
    if (changes['product'] && this.product.categoryId) {
      this.updateFilteredSubcategories(this.product.categoryId);
    }
  }

  ngOnInit() {
    this.updateFilteredSubcategories(this.product.categoryId);
  }

  updateFilteredSubcategories(categoryId: string) {
    console.log('Updating filtered subcategories for category:', categoryId);

    // Filter subcategories based on selected category
    this.filteredSubcategories = this.subcategories.filter(sub => sub.categoryId === categoryId);

    console.log('Filtered subcategories:', this.filteredSubcategories);

    // Reset subcategory if it's no longer valid
    if (!this.filteredSubcategories.some(sub => sub.id === this.product.subcategoryId)) {
      console.log('Resetting subcategory selection');
      this.product.subcategoryId = '';
    }
  }

  onCategoryChange(event: any) {
    console.log("Category changed:", event.value);
    this.product.categoryId = event.value; // Ensure categoryId updates correctly
    this.updateFilteredSubcategories(this.product.categoryId);
    this.categoryChange.emit(this.product.categoryId);
  }

  onSave() {
    this.save.emit(this.product);
  }

  onCancel() {
    this.product = { ...this.defaultProduct };
    this.cancel.emit();
  }
}