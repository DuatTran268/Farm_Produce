import React, { useEffect, useState } from "react";
import { Button, Form } from "react-bootstrap";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSave } from "@fortawesome/free-solid-svg-icons";
import { useNavigate, useParams } from "react-router-dom";
import { useSnackbar } from "notistack";
import { createNewAndUpdateComment } from "../../../api/Comment";
import FieldComment from "../../admin/edit/FieldBox";
import { faStar } from "@fortawesome/free-solid-svg-icons";
import { faStar as faStarRegular } from "@fortawesome/free-regular-svg-icons";
import "./Comment.css"

const CommentBox = () => {
  const [validated, setValidated] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const initialState = {
    id: 0,
    name: "",
    rating: 0,
    commentText: "",
  };
  const [comment, setComment] = useState(initialState);
  const navigate = useNavigate();
  let { id } = useParams();
  id = id ?? 0;

  useEffect(() => {}, []);

  const handleSubmit = (e) => {
    e.preventDefault();
    if (e.currentTarget.checkValidity() === false) {
      e.stopPropagation();
      setValidated(true);
    } else {
      let data = new FormData(e.target);
      // Lấy thông tin người dùng từ local storage hoặc từ context API
      const userId = localStorage.getItem("userId"); // Đây là ví dụ, bạn cần cập nhật phù hợp với cách lấy thông tin người dùng của bạn
      // Lấy ID của sản phẩm từ slug (giả sử bạn có một hàm để làm điều này)
      // const productId = getProductIdFromSlug(id); // Đây là ví dụ, bạn cần thay thế bằng cách lấy ID sản phẩm từ slug của bạn

      data.append("customerId", userId);
      // data.append("productId", productId);

      createNewAndUpdateComment(id, data).then((data) => {
        if (data) {
          enqueueSnackbar("Đã lưu thành công", {
            variant: "success",
          });
          navigate(`/home`);
        } else {
          enqueueSnackbar("Đã xảy ra lỗi khi lưu", {
            variant: "error",
          });
        }
      });
    }
  };

  const handleStarClick = (value) => {
    setComment({ ...comment, rating: value });
  };

  return (
    <section>
      <div className="comment_content">
        <h5 className="comment_title">Để lại bình luận của bạn</h5>
      </div>
      <div className="comment_box">
        <Form
          method="post"
          encType="multipart/form-data"
          onSubmit={handleSubmit}
          noValidate
          validated={validated}
        >
          <Form.Control type="hidden" name="id" value={comment.id} />

          <div className="mb-2 px-3">
            <span>Đánh giá:</span>{" "}
            {[1, 2, 3, 4, 5].map((value) => (
              <FontAwesomeIcon
                key={value}
                icon={value <= comment.rating ? faStar : faStarRegular}
                onClick={() => handleStarClick(value)}
                className="px-1 icon_start"
                style={{ cursor: "pointer" }}
              />
            ))}
          </div>

          <FieldComment
            control={
              <Form.Control
                className="form_control_comment"
                placeholder="Tên của bạn"
                type="text"
                name="name"
                title="Name"
                required
                value={comment.name || ""}
                onChange={(e) =>
                  setComment({ ...comment, name: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <FieldComment
            control={
              <Form.Control
                className="form_control_comment"
                placeholder="Nội dung bình luận"
                type="text"
                name="commentText"
                title="Comment Text"
                required
                value={comment.commentText || ""}
                onChange={(e) =>
                  setComment({ ...comment, commentText: e.target.value })
                }
              />
            }
            notempty={"Không được bỏ trống"}
          />

          <div className="text-center">
            <Button variant="success" type="submit">
              Bình luận
              <FontAwesomeIcon icon={faSave} className="px-2" />
            </Button>
          </div>
        </Form>
      </div>
    </section>
  );
};

export default CommentBox;
