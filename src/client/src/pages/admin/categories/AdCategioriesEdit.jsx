import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { Button, Form } from "react-bootstrap";
import BoxEdit from "../../../components/admin/edit/BoxEdit";
import BtnError from "../../../components/common/BtnError";
import { faRightFromBracket, faSave } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";
import {
  createAndUpdateCategory,
  getCategoryById,
} from "../../../api/Category";
import { isEmptyOrSpaces } from "../../../api/Utils";

const AdCategoryEdit = () => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [validated, setValidated] = useState(false);

  const initialState = {
    id: 0,
    name: "",
    urlSlug: "",
    imageUrl: "",
  };
  const navigate = useNavigate();

  const [category, setCategory] = useState(initialState);

  let { id } = useParams();
  id = id ?? 0;

  useEffect(() => {
    document.title = "Thêm/cập nhật danh mục sản phẩm";
    getCategoryById(id).then((data) => {
      if (data)
        setCategory({
          ...data,
        });
      else setCategory(initialState);
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

      createAndUpdateCategory(form).then((data) => {
        if (data) {
          console.log("data", data);
          enqueueSnackbar("Đã thêm thành công", {
            variant: "success",
          });
          navigate(`/admin/category`);
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
        <h3 className="text-success py-3">Thêm/cập nhật Danh mục sản phẩm</h3>
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={category.id} />

          <BoxEdit
            label={"Tên Category"}
            control={
              <Form.Control
                type="text"
                name="name"
                title="Name"
                required
                value={category.name || ""}
                onChange={(e) =>
                  setCategory({ ...category, name: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />


          {!isEmptyOrSpaces(category.imageUrl) && (
            <div className="row mb-3">
              <Form.Label className="col-sm-2 col-form-label">
                Hình hiện tại
              </Form.Label>
              <div className="col-sm-10">
                <img
                  src={`https://localhost:7284/${category.imageUrl}`}
                  alt={category.name}
                  height={200}
                />
              </div>
            </div>
          )}
          <BoxEdit
            label={"Chọn hình ảnh"}
            control={
              <Form.Control
                type="file"
                name="imageFile"
                accept="image/*"
                title="Image file"
                onChange={(e) =>
                  setCategory({
                    ...category,
                    imageFile: e.target.files[0],
                  })
                }
              />
            }
          />



          <div className="text-center">
            <Button variant="success" type="submit">
              Lưu các thay đổi
              <FontAwesomeIcon icon={faSave} className="px-1" />
            </Button>

            <BtnError
              icon={faRightFromBracket}
              slug={"/admin/category"}
              name="Hủy và quay lại"
            />
          </div>
        </Form>
      </div>
    </LayoutCommon>
  );
};
export default AdCategoryEdit;
