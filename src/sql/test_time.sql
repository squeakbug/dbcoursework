use pharmacy
go

-- Получить среднюю стоимость чеков, содержащих хотябы один проданный товар,
-- имеющий категорию, начинающуюся с буквы 'p',
-- произведенный до 2018 года

set statistics time on
go

select avg(receipt.card_size + receipt.cash_size)
from product join has_category on product.id = has_category.product_id
			 join category on category.id = has_category.category_id
			 join income_product on income_product.product_id = product.id
			 join mark_info on mark_info.income_product_id = income_product.id
			 join marked_product on marked_product.mark_info_id = mark_info.id
			 join receipt_marked_product rmp on rmp.marked_product_id = marked_product.id
			 join receipt on receipt.id = rmp.receipt_id
where category.title LIKE '%p%' 
	 and income_product.production_date < cast('01-01-2018' as date)
group by receipt_id
go
