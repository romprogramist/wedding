document.addEventListener("DOMContentLoaded", () => {
    console.log("product.js");
    
    const productTable = document.querySelector("table.products");
    if(productTable) {
        apiRequest("/api/product/get", "GET", null,
            (response) => {
                console.log("response", response);
                const tbody = productTable.querySelector("tbody");
                let tbodyInnerHtml = '';
                response.forEach((product) => {
                    console.log("product", product);
                    const rowHtml = `
                        <tr>
                            <td><a href="/admin/product/edit/${product.id}">${product.name}</a></td>
                            <td>${product.model}</td>
                            <td><img src="/images/products/${product.imageUrl}" alt="${product.name}"/></td>
                        </tr>
                    `;
                    tbodyInnerHtml += rowHtml;
                });
                tbody.innerHTML = tbodyInnerHtml;
            },
            (error, response) => {
                console.log("error", error);
            },
            null,
            false);    
    }
    
    const addProductForm = document.querySelector('form.product-add');
    if(addProductForm) {
        
        apiRequest("/api/category/get", "GET", null, (response) => {
            console.log("response", response);
            const select = addProductForm.querySelector("select");
            let selectInnerHtml = '';
            response.forEach((category) => {
                console.log("category", category);
                const optionHtml = `
                    <option value="${category.id}">${category.name}</option>
                `;
                selectInnerHtml += optionHtml;
            });
            select.innerHTML = selectInnerHtml;
        }, (error, response) => {
            console.log("error", error, response);
        }, null, false);
        
        addProductForm.addEventListener("submit", (e) => {
            e.preventDefault();
            const formData = new FormData();
            formData.append("name", addProductForm.elements["name"].value);
            formData.append("model", addProductForm.elements["model"].value);
            formData.append("categoryId", addProductForm.elements["categoryId"].value);
            const file = document.querySelector(".form-file").files[0];
            formData.append("file", file);

            fetch("/api/product/add", {
                method: "POST",
                body: formData
            }).then(data => {
                console.log("Success:", data);
            });
        });
    }
    
    const updateProductForm = document.querySelector('form.product-edit');
    if(updateProductForm) {
        updateProductForm.addEventListener("submit", (e) => {
            e.preventDefault();
            const formData = new FormData();
            formData.append("id", updateProductForm.elements["id"].value);
            formData.append("name", updateProductForm.elements["name"].value);
            formData.append("model", updateProductForm.elements["model"].value);
            const file = document.querySelector(".form-file").files[0];
            formData.append("file", file);

            fetch("/api/product/update", {
                method: "POST",
                body: formData
            }).then(data => {
                console.log("Success:", data);
            });
        });
    }
});

