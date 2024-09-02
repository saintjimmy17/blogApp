import { Component, AfterViewInit, ViewChild, inject } from '@angular/core';
import {MatTableModule, MatTableDataSource} from '@angular/material/table';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { Blog } from '../../../types/blog';
import { BlogService } from '../../../blog.service';

@Component({
  selector: 'app-manage-blogs',
  standalone: true,
  imports: [MatTableModule, MatPaginator, MatSort, MatFormFieldModule],
  templateUrl: './manage-blogs.component.html',
  styleUrl: './manage-blogs.component.scss'
})
export class ManageBlogsComponent {
  displayedColumns: string[] = ['title', 'category'];
  dataSource: MatTableDataSource<Blog>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor() {

    this.dataSource = new MatTableDataSource(this.blogs);
  }
  blogs:Blog[]=[];
  blogService = inject(BlogService);
  ngOnInit() {
    this.blogService.getAllBlogs().subscribe(result => {
      this.blogs = result;
      this.dataSource.data = this.blogs;
    })
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }

  getCategoryName(row: Blog) {
    return "CategoryName"
  }
}
