import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { Button, Form } from "react-bootstrap";
import {
  getFilterComboboxOfCategory,
  getFilterComboboxOfUnit,
  getProductById,
  newAndUpdateProduct,
} from "../../../api/Product";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";
import BoxEdit from "../../../components/admin/edit/BoxEdit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightToBracket, faSave } from "@fortawesome/free-solid-svg-icons";
import BtnError from "../../../components/common/BtnError";
import { format } from "date-fns";

const AdProductEdit = () => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const navigate = useNavigate();

  const initialState = {
      id: 0,
      name: "",
      quantityAvailable: 0,
      categoryId: 0,
      price: 0,
      description: "",
      status: false,
      unitId: 0,
      dateCreate: "",
      dateUpdate: "",
    },
    [filterCategory, setFilterCategory] = useState({ categoryList: [] }),
    [filterUnit, setFilterUnit] = useState({ unitList: [] });


  const [product, setProduct] = useState(initialState);

  let { id } = useParams();
  id = id ?? 0;

  useEffect(() => {
    document.title = "Thêm, cập nhật sản phẩm";
    getProductById(id).then((data) => {
      console.log("Check dataaaaaa cua id", data);
      if (data)
        setProduct({
          ...data,
        });
      else setProduct(initialState);
    });

    // filter cate
    getFilterComboboxOfCategory().then((data) => {
      if (data) {
        console.log("check data conbo category: ", data)
        setFilterCategory({
          categoryList: data.categoryList,
        });
      } else {
        setFilterCategory({ categoryList: [] });
      }
    });

    getFilterComboboxOfUnit().then((data) => {
      if (data) {
        console.log("check data conbo unit: ", data)
        setFilterUnit({
          unitList: data.unitList,
        });
      } else {
        setFilterUnit({ unitList: [] });
      }
    });

  }, []);

  const [validated, setValidated] = useState(false);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      setValidated(true);
    } else {
      let form = new FormData(e.target);
      form.append("status", product.status);

      newAndUpdateProduct(form).then((data) => {
        if (data) {
          console.log("Check data update end create", data)
          enqueueSnackbar("Đã lưu thành công", {
            variant: "success",
          });
          navigate(`/admin/product`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi", {
            variant: "error",
            closeSnackbar,
          });
        }
      });
    }
  };

  return (
    <LayoutCommon>
      <div className="wrapper">
        <h3 className="text-success py-3">Thêm/cập Product</h3>
        <Form
          method="post"
          encType=""
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={product.id} />

          <BoxEdit
            label={"Tên Sản phẩm"}
            control={
              <Form.Control
                type="text"
                name="name"
                title="Name"
                required
                value={product.name || ""}
                onChange={(e) =>
                  setProduct({ ...product, name: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Số lượng"}
            control={
              <Form.Control
                type="number"
                name="quantityAvailable"
                title="quantity Available"
                required
                value={product.quantityAvailable || ""}
                onChange={(e) =>
                  setProduct({ ...product, quantityAvailable: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">
              Category
            </Form.Label>
            <div className="col-sm-10">
              <Form.Select
                name="categoryId"
                title="category Id"
                value={product.categoryId}
                required
                onChange={(e) =>
                  setProduct({
                    ...product,
                    categoryId: e.target.value,
                  })
                }
              >
                {filterCategory.categoryList.length > 0 &&
                  filterCategory.categoryList.map((item, index) => (
                    <option key={index} value={item.value}>
                      {item.text}
                    </option>
                  ))}
              </Form.Select>
              <Form.Control.Feedback type="invalid">
                Không được bỏ trống.
              </Form.Control.Feedback>
            </div>
          </div>

          <BoxEdit
            label={"Giá tiền"}
            control={
              <Form.Control
                type="number"
                name="price"
                title="price"
                required
                value={product.price || ""}
                onChange={(e) =>
                  setProduct({ ...product, price: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Mô tả"}
            control={
              <Form.Control
                type="text"
                name="description"
                title="description"
                required
                value={product.description || ""}
                onChange={(e) =>
                  setProduct({ ...product, description: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <div className="row mb-3">
            <Form.Label className="col-sm-2 col-form-label">
              Unit
            </Form.Label>
            <div className="col-sm-10">
              <Form.Select
                name="unitId"
                title="Unit"
                value={product.unitId}
                required
                onChange={(e) =>
                  setProduct({
                    ...product,
                    unitId: e.target.value,
                  })
                }
              >
                {filterUnit.unitList.length > 0 &&
                  filterUnit.unitList.map((item, index) => (
                    <option key={index} value={item.value}>
                      {item.text}
                    </option>
                  ))}
              </Form.Select>
              <Form.Control.Feedback type="invalid">
                Không được bỏ trống.
              </Form.Control.Feedback>
            </div>
          </div>

          <BoxEdit
            label={"Ngày tạo"}
            control={
              <Form.Control
                type="datetime-local"
                name="dateCreate"
                title="Date Create"
                required
                value={product.dateCreate || ""}
                onChange={(e) =>
                  setProduct({
                    ...product,
                    dateCreate: e.target.value,
                  })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Ngày cập nhật"}
            control={
              <Form.Control
                type="datetime-local"
                name="dateUpdate"
                title="date Update"
                required
                value={product.dateUpdate || ""}
                onChange={(e) =>
                  setProduct({ ...product, dateUpdate: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <div className="row mb-3">
            <div className="col-sm-10 offset-sm-2">
              <div className="form-check">
                <input
                  className="form-check-input"
                  type="checkbox"
                  name="status"
                  checked={product.status}
                  title="Hiển thị"
                  onChange={(e) =>
                    setProduct({ ...product, status: e.target.checked })
                  }
                />
                <Form.Label className="form-check-label">Hiển thị</Form.Label>
              </div>
            </div>
          </div>

          <div className="text-center">
            <Button variant="success" type="submit">
              Lưu các thay đổi
              <FontAwesomeIcon icon={faSave} className="px-1" />
            </Button>

            <BtnError
              icon={faRightToBracket}
              slug={"/admin/product"}
              name="Hủy và quay lại"
            />
          </div>
        </Form>
      </div>
    </LayoutCommon>
  );
};
export default AdProductEdit;
