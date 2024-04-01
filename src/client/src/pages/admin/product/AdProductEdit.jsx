import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { Button, Form } from "react-bootstrap";
import { getProductById, newAndUpdateProduct } from "../../../api/Product";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";
import BoxEdit from "../../../components/admin/edit/BoxEdit";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightToBracket, faSave } from "@fortawesome/free-solid-svg-icons";
import BtnError from "../../../components/common/BtnError";

const AdProductEdit = () => {
  const [validated, setValidated] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const initialState = {
    id: 0,
    name: "",
    quantityAvailable: "",
    categoryId: 0,
    price: 0,
    description: "",
    status: 0,
    unitId: 0,
    dateCreate: "",
    dateUpdate: "",
  };
  const navigate = useNavigate();
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
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      let form = new FormData(e.target);
      console.log("form", form);

      newAndUpdateProduct(form).then((data) => {
        if (data) {
          console.log("data", data);
          enqueueSnackbar("Đã thêm thành công", {
            variant: "success",
          });
          navigate(`/admin/product`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi", {
            variant: "error",
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
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={product.id} />

          <BoxEdit
            label={"Tên sản phẩm"}
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
                type="text"
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

          <BoxEdit
            label={"Category Id"}
            control={
              <Form.Control
                type="text"
                name="categoryId"
                title="category Id"
                required
                value={product.categoryId || ""}
                onChange={(e) =>
                  setProduct({ ...product, categoryId: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Giá tiền"}
            control={
              <Form.Control
                type="text"
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

          <BoxEdit
            label={"Trạng thái"}
            control={
              <Form.Control
                type="text"
                name="status"
                title="status"
                required
                value={product.status || ""}
                onChange={(e) =>
                  setProduct({ ...product, status: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Unit Id"}
            control={
              <Form.Control
                type="text"
                name="unitId"
                title="unit Id"
                required
                value={product.unitId || ""}
                onChange={(e) =>
                  setProduct({ ...product, unitId: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Ngày tạo"}
            control={
              <Form.Control
                type="text"
                name="dateCreate"
                title="dateCreate"
                required
                value={product.dateCreate || ""}
                onChange={(e) =>
                  setProduct({ ...product, dateCreate: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Ngày cập nhật"}
            control={
              <Form.Control
                type="text"
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
