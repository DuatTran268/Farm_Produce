import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { useNavigate, useParams } from "react-router-dom";
import { Button, Form } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faRightFromBracket, faSave } from "@fortawesome/free-solid-svg-icons";
import BoxEdit from "../../../components/admin/edit/BoxEdit";
import BtnError from "../../../components/common/BtnError";
import { useSnackbar } from "notistack";
import { createAndUpdateImage, getImageById } from "../../../api/Image";
import { isEmptyOrSpaces } from "../../../api/Utils";

const AdImagesEdit = () => {
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [validated, setValidated] = useState(false);

  const initialState = {
    id: 0,
    name: "",
    urlImage: "",
    productId: 0,
  };
  const navigate = useNavigate();

  const [Image, setImage] = useState(initialState);

  let { id } = useParams();
  id = id ?? 0;

  useEffect(() => {
    document.title = "Thêm, cập nhật Image";
    getImageById(id).then((data) => {
      console.log("Checkddd dataa image", data);
      if (data)

        setImage({
          ...data,
        });
      else setImage(initialState);
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

      createAndUpdateImage(form).then((data) => {
        if (data) {
          console.log("dtaaaa imgae", data);
          enqueueSnackbar("Đã thêm thành công", {
            variant: "success",
          });
          navigate(`/admin/Image`);
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
        <h3 className="text-success py-3">Thêm/cập Hình ảnh cho sản phẩm</h3>
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={Image.id} />

          <BoxEdit
            label={"Tên Hình ảnh"}
            control={
              <Form.Control
                type="text"
                name="name"
                title="Name"
                required
                value={Image.name || ""}
                onChange={(e) => setImage({ ...Image, name: e.target.value })}
              />
            }
            notempty={"Không được bỏ trống"}
          />

          
          {!isEmptyOrSpaces(Image.urlImage) && (
            <div className="row mb-3">
              <Form.Label className="col-sm-2 col-form-label">
                Hình hiện tại
              </Form.Label>
              <div className="col-sm-10">
                <img
                  src={`https://localhost:7047/${Image.urlImage}`}
                  alt={Image.name}
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
                  setImage({
                    ...Image,
                    imageFile: e.target.files[0],
                  })
                }
              />
            }
          />

          <BoxEdit
            label={"Mã sản phẩm"}
            control={
              <Form.Control
                type="text"
                name="productId"
                title="Product Id"
                required
                value={Image.productId || ""}
                onChange={(e) => setImage({ ...Image, productId: e.target.value })}
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
              icon={faRightFromBracket}
              slug={"/admin/image"}
              name="Hủy và quay lại"
            />
          </div>
        </Form>
      </div>
    </LayoutCommon>
  );
};
export default AdImagesEdit;
