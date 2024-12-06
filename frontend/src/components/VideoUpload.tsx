import React, { useState } from 'react';
import axios from 'axios';

const VideoUpload = () => {
    const [file, setFile] = useState<File | null>(null);
    const [loading, setLoading] = useState(false);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!file) return;

        setLoading(true);
        const formData = new FormData();
        formData.append('VideoContent', file);
        formData.append('Title', 'My Video Post');
        formData.append('Description', 'Video description');
        formData.append('CreatorId', 'creator123'); // Get from auth

        try {
            await axios.post('http://localhost:5217/api/post', formData, {
                headers: {
                    'Content-Type': 'multipart/form-data',
                }
            });
            alert('Video uploaded successfully!');
        } catch (error) {
            console.error('Upload failed:', error);
            alert('Upload failed');
        } finally {
            setLoading(false);
        }
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="file"
                accept="video/*"
                onChange={(e) => setFile(e.target.files?.[0] || null)}
            />
            <button type="submit" disabled={!file || loading}>
                {loading ? 'Uploading...' : 'Upload Video'}
            </button>
        </form>
    );
};

export default VideoUpload;