import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { useSnackbar } from "notistack";
import { Link, useNavigate, useParams } from "react-router-dom";
import { getUnitById, newAndUpdateUnit } from "../../../api/Unit";
import { Button, Form } from "react-bootstrap";
import BoxEdit from "../../../components/admin/edit/BoxEdit";
import BtnError from "../../../components/common/BtnError";
import { faArrowRotateBack, faBackward, faBackwardStep, faCheck, faCheckCircle, faRightToBracket, faSave } from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

const AdUnitEdit = () => {
  const [validated, setValidated] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const initialState = {
      id: 0,
      name: "",
      urlSlug: "",
    },
    [unit, setUnit] = useState(initialState);

  const navigate = useNavigate();

  let { id } = useParams();
  id = id ?? 0;

  useEffect(() => {
    document.title = "Thêm, cập nhật đơn vị tính";
    getUnitById(id).then((data) => {
      if (data) {
        setUnit(data);
      } else {
        setUnit(initialState);
      }
    });
  }, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      let data = new FormData(e.target);

      newAndUpdateUnit(id, data).then((data) => {
        if (data) {
          enqueueSnackbar("Đã lưu thành công", {
            variant: "success",
          });
          navigate(`/admin/unit`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi khi lưu", {
            variant: "error",
          });
        }
      });
    }
  };

  return (
    <LayoutCommon>
      <div className="wrapper">
        <h3 className="text-success py-3">Thêm/cập nhật đơn vị tính</h3>
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={unit.id} />

          <BoxEdit
            label={"Tên đơn vị tính"}
            control={
              <Form.Control
                type="text"
                name="name"
                title="Name"
                required
                value={unit.name || ""}
                onChange={(e) => setUnit({ ...unit, name: e.target.value })}
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <BoxEdit
            label={"Url Slug"}
            control={
              <Form.Control
                type="text"
                name="urlSlug"
                title="url Slug"
                required
                value={unit.urlSlug || ""}
                onChange={(e) => setUnit({ ...unit, urlSlug: e.target.value })}
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <div className="text-center">
            <Button variant="success" type="submit">
              Lưu các thay đổi 
              <FontAwesomeIcon icon={faSave} className="px-1"/>
            </Button>

            <BtnError
              icon={faRightToBracket}
              slug={"/admin/unit"}
              name="Hủy và quay lại"
            />
          </div>
        </Form>
      </div>
    </LayoutCommon>
  );
};
export default AdUnitEdit;
