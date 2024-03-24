import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { useSnackbar } from "notistack";
import { useNavigate, useParams } from "react-router-dom";



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
    document.title = "Thêm, cập nhật Unit";
    getUnitById(id).then((data) => {
      if (data) {
        setUnit(data);
      } else {
        setUnit(initialState);
      }
    });
  }, []);




  return (
    <LayoutCommon>
      {/* <div className="researcher-wrapper">
          <h3 className="text-success py-3">Thêm/cập nhật nhà khoa học</h3>
          <Form
            method="post"
            encType="multipart/form-data"
            onSubmit={handleSubmit}
            noValidate
            validated={validated}
          >
            <Form.Control type="hidden" name="id" value={researcher.id} />
            <div className="row mb-3">
              <Form.Label className="col-sm-2 col-form-label">
                Tên nhà khoa học
              </Form.Label>
              <div className="col-sm-10">
                <Form.Control
                  type="text"
                  name="name"
                  title="Name"
                  required
                  value={researcher.name || ""}
                  onChange={(e) =>
                    setResearcher({ ...researcher, name: e.target.value })
                  }
                />
                <Form.Control.Feedback type="invalid">
                  Không được bỏ trống.
                </Form.Control.Feedback>
              </div>
            </div>

            <div className="text-center">
              <Button variant="success" type="submit">
                Lưu các thay đổi
              </Button>
              <Link to="/admin/researcher" className="btn btn-danger ms-2">
                Hủy và quay lại
              </Link>
            </div>
          </Form>
        </div> */}
    </LayoutCommon>
  )
}
export default AdUnitEdit;