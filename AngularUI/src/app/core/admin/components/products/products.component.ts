import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule, TableLazyLoadEvent } from 'primeng/table';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { TableFilterService } from '../../services/table-filter.service';
import { ProductDto } from '../../interfaces/product-dto';
import { ToolbarModule } from 'primeng/toolbar';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FieldsetModule } from 'primeng/fieldset';
import { CategoryService } from '../../services/category.service';
import { CategoryDto } from '../../interfaces/category-dto';
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { SubcategoryDto } from '../../interfaces/subcategory-dto';
import { SubcategoryService } from '../../services/subcategory.service';
import { environment } from '../../../../../environments/environment.development';
import { FileUploadHandlerEvent, FileUploadModule } from 'primeng/fileupload';
import { ProductImageUploadDto } from '../../interfaces/product-image-upload-dto';
import { ProductFormComponent } from './product-form/product-form.component';

@Component({
  selector: 'app-products',
  standalone: true,
  providers: [TableFilterService],
  imports: [
    CommonModule,
    TableModule,
    InputTextModule,
    DialogModule,
    ButtonModule,
    ToolbarModule,
    FormsModule,
    FieldsetModule,
    ReactiveFormsModule,
    DropdownModule,
    MultiSelectModule,
    FileUploadModule,
    ProductFormComponent
  ],
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent {
  productService: ProductService;
  categoryService: CategoryService;
  subcategoryService: SubcategoryService;
  router: Router;
  tableFilter: TableFilterService;
  loading: boolean = true;
  apiUrl: string = environment.apiUrl;

  products: ProductDto[] = [];
  categories: CategoryDto[] = [];
  categoryNames: string[] = [];
  subcategories: SubcategoryDto[] = [];
  filteredSubcategories: SubcategoryDto[] = [];

  totalRecords: number = 100;

  productFormVisible: boolean = false;
  isFormEditing: boolean = false;
  product: ProductDto = {
    name: '',
    description: '',
    price: 0,
    stock: 0,
    discount: 0,
    categoryId: '',
    categoryName: '',
    subcategoryId: '',
    subcategoryName: ''
  };

  lastEvent: TableLazyLoadEvent = {};
  categoryNamesToFilter: string[] = [];

  imageUploadVisible: boolean = false;
  imageProduct: ProductDto | undefined;

  constructor(productService: ProductService, categoryService: CategoryService, subcategoryService: SubcategoryService, router: Router, tableFilter: TableFilterService) {
    this.productService = productService;
    this.categoryService = categoryService;
    this.subcategoryService = subcategoryService;
    this.router = router;
    this.tableFilter = tableFilter;
  }

  ngOnInit() {
    this.getCategories();
    this.getSubcategories();
  }

  loadData(event: TableLazyLoadEvent) {
    if (event.filters) {
      this.tableFilter.applyFilters(event);
    }

    this.tableFilter.applyPaginationAndSorting(event);

    this.getProducts();

    this.loading = false;
    this.lastEvent = event;
  }

  getProducts() {
    this.loading = true;
    this.productService.getFiltered(
      this.tableFilter.filtersString,
      this.tableFilter.page,
      this.tableFilter.pageSize,
      this.tableFilter.sortField,
      this.tableFilter.sortOrder)
      .subscribe((response: { totalCount: number, data: ProductDto[] }) => {
        this.products = response.data;
        this.totalRecords = response.totalCount;
        this.loading = false;
      });      
  }

  getCategories() {
    this.categoryService.getAll().subscribe((categories: CategoryDto[]) => {
      this.categories = categories;
      this.categoryNames = categories.map(category => category.name);
    });
  }

  getSubcategories() {
    this.subcategoryService.getAll().subscribe((subcategories: SubcategoryDto[]) => {
      this.subcategories = subcategories;
    });
  }

  updateSubcategoryDropdown(categoryId: string) {
    console.log('Updating subcategories for category id:', categoryId);
    this.filteredSubcategories = this.subcategories.filter(subcategory => subcategory.categoryId === categoryId);
    console.log('Filtered subcategories:', this.filteredSubcategories);
  }

  addProduct() {
    if (!this.product.categoryId || !this.product.subcategoryId) {
      return;
    }

    return this.productService.create(this.product).subscribe(() => {
      this.getProducts();
      this.toggleFormDialog();
    });
  }

  editProduct(product: ProductDto) {
    this.product = { ...product };
    this.updateSubcategoryDropdown(this.product.categoryId); // Update subcategories when editing
    this.isFormEditing = true;
    this.toggleFormDialog();
  }

  deleteProduct(product: ProductDto) {
    if (!product.id) {
      return;
    }
    this.productService.delete(product.id).subscribe(() => {
      this.getProducts();
    });
  }

  resetForm() {
    this.product = {
      name: '',
      description: '',
      price: 0,
      stock: 0,
      discount: 0,
      categoryId: '',
      categoryName: '',
      subcategoryId: '',
      subcategoryName: ''
    };
  }

  toggleFormDialog() {
    if (this.productFormVisible) {
      this.resetForm();
    }
    this.productFormVisible = !this.productFormVisible;
    if (this.isFormEditing) {
      this.isFormEditing = false;
    }    
  }

  getImage(product: ProductDto) {
    return 'https://localhost:2048/' + product.imageUrl;
  }

  uploadImage(product: ProductDto) {
    this.imageProduct = product;
    this.toggleImageUploadDialog();
  }

  onUpload($event: FileUploadHandlerEvent) {
    const file = $event.files[0];

    if (!file || !this.imageProduct?.id) {
      return;
    }
    const imageUploadDto : ProductImageUploadDto = {
      productId: this.imageProduct.id,
      image: file,
    };

    this.productService.uploadImage(imageUploadDto).subscribe((imageUrl: string) => {
      this.toggleImageUploadDialog();
    });
  }

  toggleImageUploadDialog() {
    this.imageUploadVisible = !this.imageUploadVisible;
  }

  get chosenCategory(): CategoryDto {
    return { name: this.product.categoryName || '', id: this.product.categoryId };
  }

  get chosenSubcategory(): SubcategoryDto {
    return { name: this.product.subcategoryName || '', id: this.product.subcategoryId };
  }
}